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
import { MomService } from 'src/app/Services/momService';
import { convertToDate, dateFormatValidator } from 'src/app/utils/dateFormatValidator';

@Component({
  standalone: true,
  selector: 'project-route',
  templateUrl: './moms.component.html',
  styleUrls: ['./moms.component.scss'],
  imports: [RouterLink, ReactiveFormsModule],
})
export class MomsComponent implements OnInit {
  data: Array<MeetingMinute> = [];
  projectId: string = '';
  forms: FormGroup = new FormGroup({
    formitem: new FormGroup({
      date: new FormControl(''),
      duration: new FormControl(''),
      links: new FormControl(''),
      comments: new FormControl(''),
    }),
  });
  unauthorizedPerson: boolean = true;
  displayedColumns = ['Date', 'Duration', 'MoM Link', 'Comments'];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private meetingMinuteService: MomService
  ) {
    this.projectId = this.route.snapshot.pathFromRoot[1].params['id'];
  }

  ngOnInit(): void {
    this.meetingMinuteService.getAllItem().subscribe(
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
      date: ['', [Validators.required, dateFormatValidator()]],
      duration: ['', Validators.required],
      links: ['', Validators.required],
      comments: ['', Validators.required],
    });
  }

  existingDataFormGroup(e: any): FormGroup {
    return this.fb.group({
      id: [e.id],
      date: [convertToDate(e.meetingDate), [Validators.required, dateFormatValidator()]],

      duration: [e.duration, Validators.required],
      links: [e.moMLink, Validators.required],
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
    this.meetingMinuteService.deleteItem(controlAtIndex.value.id).subscribe(
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
          const modelDate: MeetingMinute = {
            projectId: this.projectId,
            duration: e.duration,
            meetingDate: e.date,
            moMLink: e.links,
            comments: e.comments,
          };
          console.log(e);
          if (e.id != '') {
            const data = await this.meetingMinuteService.updateItem(e.id, modelDate).toPromise();
            console.log(data);
          } else {
            const data = await this.meetingMinuteService.createItem(modelDate).toPromise();
            console.log(data);
          }
        } catch (error) {
          console.error('Error:', error);
        }
      });
    }
  }
}
