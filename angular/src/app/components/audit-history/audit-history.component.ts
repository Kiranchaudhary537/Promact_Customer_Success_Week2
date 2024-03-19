import { NgFor } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { AuditHistoryModel } from 'src/app/Model/Models';
import { MeetingMinute } from 'src/app/Model/MomModel';
import { AuditHistoryService } from 'src/app/Services/auditHistoryService';
import { MomService } from 'src/app/Services/momService';
import { convertToDate, dateFormatValidator } from 'src/app/utils/dateFormatValidator';

@Component({
  standalone: true,
  selector: 'project-route',
  templateUrl: './audit-history.component.html',
  styleUrls: ['./audit-history.component.scss'],
  imports: [RouterLink, ReactiveFormsModule],
})
export class AuditHistoryComponent implements OnInit {
  data: Array<AuditHistoryModel> = [];
  projectId: string = '';
  forms: FormGroup = new FormGroup({
    formitem: new FormGroup({
      dateOfAudit: new FormControl(''),
      reviewedBy: new FormControl(''),
      status: new FormControl(''),
      section: new FormControl(''),
      commentQueries: new FormControl(''),
      actionItem: new FormControl(''),
    }),
  });
  unauthorizedPerson: boolean = true;
  displayedColumns = [
    'Date Of Audit',
    'Reviewed By',
    'Status',
    'Section',
    'Comment Queries',
    'Action Item',
  ];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private auditHistoryService: AuditHistoryService
  ) {
    this.projectId = this.route.snapshot.pathFromRoot[1].params['id'];
  }

  ngOnInit(): void {
    this.auditHistoryService.getAllItem().subscribe(
      data => {
        console.log('audit:', data);
        this.data = data.filter(item => item?.projectId == this.projectId);
        this.addExistingData(data.filter(item => item?.projectId == this.projectId));
      },
      error => {
        this.addExistingData([]);
        if (error.status == 403) {
          this.unauthorizedPerson = false;
          console.log(this.unauthorizedPerson);
          console.warn('Unauthorized access (403):', error);
        } else {
          console.error('Error fetching projects:', error);
        }
      }
    );
  }

  addExistingData(data: any): void {
    let existingData: any = [];
    data.forEach((element: any) => {
      existingData.push(this.existingDataFormGroup(element));
    });
    this.forms = this.fb.group({
      formitem:
        existingData == 0 ? this.fb.array([this.createFormGroup()]) : this.fb.array(existingData),
    });
  }

  createFormGroup(): FormGroup {
    return this.fb.group({
      id: [''],
      dateOfAudit: ['', [Validators.required, dateFormatValidator()]],
      reviewedBy: ['', Validators.required],
      status: ['', Validators.required],
      section: ['', Validators.required],
      commentQueries: ['', Validators.required],
      actionItem: ['', Validators.required],
    });
  }

  existingDataFormGroup(e: any): FormGroup {
    return this.fb.group({
      id: [e.id],
      dateOfAudit: [convertToDate(e.dateOfAudit), [Validators.required, dateFormatValidator()]],
      reviewedBy: [e.reviewedBy, Validators.required],
      status: [e.status, Validators.required],
      section: [e.section, Validators.required],
      commentQueries: [e.commentQueries, Validators.required],
      actionItem: [e.actionItem, Validators.required],
    });
  }

  getformitems(): FormArray {
    return this.forms.get('formitem') as FormArray;
  }

  addRow(): void {
    const approveteamArray = this.forms.get('formitem') as FormArray;
    approveteamArray.push(this.createFormGroup());
  }

  removeRow(index: number): void {
    const approveteamArray = this.forms.get('formitem') as FormArray;
    const controlAtIndex = approveteamArray.at(index);
    this.auditHistoryService.deleteItem(controlAtIndex.value.id).subscribe(
      data => {
        console.log(data);
      },
      error => {
        console.error('erorr');
      }
    );
    approveteamArray.removeAt(index);
  }

  onSubmit(): void {
    if (this.forms.valid) {
      this.forms.value.formitem.forEach(async e => {
        try {
          const modelDate: AuditHistoryModel = {
            projectId: this.projectId,
            dateOfAudit: e.dateOfAudit,
            reviewedBy: e.reviewedBy,
            status: e.status,
            commentQueries: e.commentQueries,
            section: e.section,
            actionItem: e.actionItem,
          };
          console.log(modelDate);
          if (e.id != '') {
            const data = await this.auditHistoryService.updateItem(e.id, modelDate).toPromise();
            console.log(data);
          } else {
            const data = await this.auditHistoryService.createItem(modelDate).toPromise();
            console.log(data);
          }
        } catch (error) {
          console.error('Error:', error);
        }
      });
    }
    else{
      alert("Make sure you filled right value, check date formate");
    }
  }
}
