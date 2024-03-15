import { NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';
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
import { ProjectUpdate } from 'src/app/Model/ProjectUpdateModel';
import { ProjectUpdateService } from 'src/app/Services/projectUpdateService';
import { convertToDate, dateFormatValidator } from 'src/app/utils/dateFormatValidator';

@Component({
  standalone: true,
  selector: 'project-route',
  templateUrl: './project-updates.component.html',
  styleUrls: ['./project-updates.component.scss'],
  imports: [RouterLink, ReactiveFormsModule, NgFor],
})
export class ProjectUpdatesComponent implements OnInit {
  data: Array<ProjectUpdate> = [];
  projectId: string = '';
  forms: FormGroup = new FormGroup({
    items: new FormGroup({
      name: new FormControl(''),
      role: new FormControl(''),
      startDate: new FormControl(''),
      endDate: new FormControl(''),
      allocationPercentage: new FormControl(''),
      comment: new FormControl(''),
    }),
  });
  unauthorizedPerson: boolean = true;
  displayedColumns = ['Date', 'General Updates'];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private projectUpdateService: ProjectUpdateService
  ) {
    this.projectId = this.route.snapshot.pathFromRoot[1].params['id'];
  }

  ngOnInit(): void {
    this.projectUpdateService.getAllItem().subscribe(
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
      items:
        existingData == 0 ? this.fb.array([this.createFormGroup()]) : this.fb.array(existingData),
    });
  }

  createFormGroup(): FormGroup {
    return this.fb.group({
      id: [''],
      date: ['', [Validators.required, dateFormatValidator()]],
      updates: ['', Validators.required],
    });
  }

  existingDataFormGroup(e: any): FormGroup {
    return this.fb.group({
      id: [e.id],
      date: [convertToDate(e.date), [Validators.required, dateFormatValidator()]],
      updates: [e.generalUpdates, Validators.required],
    });
  }

  getformitems(): FormArray {
    return this.forms.get('items') as FormArray;
  }

  addRow(): void {
    const approveteamArray = this.forms.get('items') as FormArray;
    approveteamArray.push(this.createFormGroup());
  }

  removeRow(index: number): void {
    const approveteamArray = this.forms.get('items') as FormArray;
    approveteamArray.removeAt(index);
  }

  onSubmit(): void {
    if (this.forms.valid) {
      this.forms.value.items.forEach(async e => {
        try {
          const modelDate: ProjectUpdate = {
            projectId: this.projectId,
            date: e.date,
            generalUpdates: e.updates,
          };
          console.log(e);
          if (e.id == '') {
            const data = await this.projectUpdateService.createItem(modelDate).toPromise();
            console.log(data);
          } else {
            const data = await this.projectUpdateService.updateItem(e.id, modelDate).toPromise();
            console.log(data);
          }
        } catch (error) {
          console.error('Error:', error);
        }
      });
    }
  }
}
