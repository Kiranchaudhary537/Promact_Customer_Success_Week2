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
import { AuditHistoryModel, VersionHistoryModel } from 'src/app/Model/Models';
import { MeetingMinute } from 'src/app/Model/MomModel';
import { MomService } from 'src/app/Services/momService';
import { VersionHistoryService } from 'src/app/Services/versionHistoryService';
import { convertToDate, dateFormatValidator } from 'src/app/utils/dateFormatValidator';

@Component({
  standalone: true,
  selector: 'project-route',
  templateUrl: './version-history.component.html',
  styleUrls: ['./version-history.component.scss'],
  imports: [RouterLink, ReactiveFormsModule],
})
export class VersionHistoryComponent implements OnInit {
  data: Array<VersionHistoryModel> = [];
  projectId: string = '';
  forms: FormGroup = new FormGroup({
    formitem: new FormGroup({
      version: new FormControl(''),
      change: new FormControl(''),
      changeReason: new FormControl(''),
      section: new FormControl(''),
      revisionDate: new FormControl(''),
      approvalDate: new FormControl(''),
      approvedBy: new FormControl(''),
    }),
  });
  unauthorizedPerson: boolean = true;

  displayedColumns = [
    'Version',
    'Change',
    'Change Reason',
    'Created By',
    'Revision Date',
    'Approval Date',
    'Approved By',
  ];
  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private versionHistoryService: VersionHistoryService
  ) {
    this.projectId = this.route.snapshot.pathFromRoot[1].params['id'];
  }

  ngOnInit(): void {
    this.versionHistoryService.getAllItem().subscribe(
      data => {
        console.log('Projects:', data);
        this.data = data;
        this.addExistingData(data);
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
      version: ['', Validators.required],
      change: ['', Validators.required],
      changeReason: ['', Validators.required],
      createdBy: ['', Validators.required],
      revisionDate: ['', [Validators.required, dateFormatValidator()]],
      approvalDate: ['', [Validators.required, dateFormatValidator()]],
      approvedBy: ['', Validators.required],
    });
  }

  existingDataFormGroup(e: any): FormGroup {
    return this.fb.group({
      id: [e.id],
      version: [e.version, Validators.required],
      change: [e.change, Validators.required],
      changeReason: [e.changeReason, Validators.required],
      createdBy: [e.createdBy, Validators.required],
      revisionDate: [convertToDate(e.revisionDate), [Validators.required, dateFormatValidator()]],
      approvalDate: [convertToDate(e.approvalDate), [Validators.required, dateFormatValidator()]],
      approvedBy: [e.approvedBy, Validators.required],
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
    this.versionHistoryService.deleteItem(controlAtIndex.value.id).subscribe(
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
          const modelDate: VersionHistoryModel = {
            projectId: this.projectId,
            version: e.version,
            change: e.change,
            changeReason: e.changeReason,
            createdBy: e.createdBy,
            revisionDate: e.revisionDate,
            approvalDate: e.approvalDate,
            approvedBy: e.approvedBy,
          };
          console.log(modelDate);
          if (e.id != '') {
            const data = await this.versionHistoryService.updateItem(e.id, modelDate).toPromise();
            console.log(data);
          } else {
            const data = await this.versionHistoryService.createItem(modelDate).toPromise();
            console.log(data);
          }
        } catch (error) {
          console.error('Error:', error);
        }
      });
    }
  }
}
