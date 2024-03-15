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
import { StakeholderModel } from 'src/app/Model/Models';
import { MeetingMinute } from 'src/app/Model/MomModel';
import { MomService } from 'src/app/Services/momService';
import { StakeholderService } from 'src/app/Services/stakeholderService';
import { convertToDate, dateFormatValidator } from 'src/app/utils/dateFormatValidator';

@Component({
  standalone: true,
  selector: 'project-route',
  templateUrl: './stakeholder.component.html',
  styleUrls: ['./stakeholder.component.scss'],
  imports: [RouterLink, ReactiveFormsModule],
})
export class StakeholderComponent implements OnInit {
  data: Array<StakeholderModel> = [];
  projectId: string = '';
  forms: FormGroup = new FormGroup({
    formitem: new FormGroup({
      title: new FormControl(''),
      name: new FormControl(''),
      contactEmail: new FormControl(''),
    }),
  });
  unauthorizedPerson: boolean = true;
  displayedColumns = ['Title', 'Name', 'Email'];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private stakeholderService: StakeholderService
  ) {
    this.projectId = this.route.snapshot.pathFromRoot[1].params['id'];
  }

  ngOnInit(): void {
    this.stakeholderService.getAllItem().subscribe(
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
      title: ['', Validators.required],
      name: ['', Validators.required],
      contactEmail: ['', [Validators.required, Validators.email]],
    });
  }

  existingDataFormGroup(e: any): FormGroup {
    return this.fb.group({
      id: [e.id],
      title: [e.title, Validators.required],
      name: [e.name, Validators.required],
      contactEmail: [e.contactEmail, [Validators.required, Validators.email]],
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
    this.stakeholderService.deleteItem(controlAtIndex.value.id).subscribe(
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
          const modelDate: StakeholderModel = {
            projectId: this.projectId,
            title: e.title,
            name: e.name,
            contactEmail: e.contactEmail,
          };
          if (e.id != '') {
            const data = await this.stakeholderService.updateItem(e.id, modelDate).toPromise();
            console.log(data);
          } else {
            const data = await this.stakeholderService.createItem(modelDate).toPromise();
            console.log(data);
          }
        } catch (error) {
          console.error('Error:', error);
        }
      });
    }
  }
}
