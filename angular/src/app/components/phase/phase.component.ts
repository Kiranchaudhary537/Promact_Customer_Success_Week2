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
import { MeetingMinute } from 'src/app/Model/MomModel';
import { Phase } from 'src/app/Model/PhaseModel';
import { MomService } from 'src/app/Services/momService';
import { PhaseService } from 'src/app/Services/phaseSerivce';
import { convertToDate, dateFormatValidator } from 'src/app/utils/dateFormatValidator';

@Component({
  standalone: true,
  selector: 'project-route',
  templateUrl: './phase.component.html',
  styleUrls: ['./phase.component.scss'],
  imports: [RouterLink, ReactiveFormsModule],
})
export class PhaseComponent implements OnInit {
  data: Array<Phase> = [];
  projectId: string = '';
  forms: FormGroup = new FormGroup({
    formitem: new FormGroup({
      title: new FormControl(''),
      startDate: new FormControl(''),
      completionDate: new FormControl(''),
      approvalDate: new FormControl(''),
      status: new FormControl(''),
      comments: new FormControl(''),
    }),
  });
  unauthorizedPerson: boolean = true;
  displayedColumns = ['Date', 'Duration', 'MoM Link', 'Comments'];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private phaseService: PhaseService
  ) {
    this.projectId = this.route.snapshot.pathFromRoot[1].params['id'];
  }

  ngOnInit(): void {
    this.phaseService.getAllItem().subscribe(
      data => {
        console.log('Projects:', data);
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
      title: ['', Validators.required],
      startDate: ['', [Validators.required, dateFormatValidator()]],
      completionDate: ['', [Validators.required, dateFormatValidator()]],
      approvalDate: ['', [Validators.required, dateFormatValidator()]],
      status: ['', Validators.required],
      comments: ['', Validators.required],
    });
  }

  existingDataFormGroup(e: any): FormGroup {

    return this.fb.group({
      id: [e.id],
      title: [e.title, Validators.required],
      startDate: [convertToDate(e.startDate), [Validators.required, dateFormatValidator()]],
      completionDate: [
        convertToDate(e.completionDate),
        [Validators.required, dateFormatValidator()],
      ],
      approvalDate: [convertToDate(e.approvalDate), [Validators.required, dateFormatValidator()]],
      status: [e.status, Validators.required],
      comments: [e.comments, Validators.required],
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
    this.phaseService.deleteItem(controlAtIndex.value.id).subscribe(
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
          const modelDate: Phase = {
            projectId: this.projectId,
            title: e.title,
            startDate: e.startDate,
            completionDate: e.completionDate,
            approvalDate: e.approvalDate,
            status: e.status,
            comments: e.comments,
          };
          console.log(e);
          if (e.id != '') {
            const data = await this.phaseService.updateItem(e.id, modelDate).toPromise();
            console.log(data);
          } else {
            const data = await this.phaseService.createItem(modelDate).toPromise();
            console.log(data);
          }
        } catch (error) {
          console.error('Error:', error);
        }
      });
    }
  }
}
