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
import { projectResourceModel } from 'src/app/Model/ProjectResourceModel';
import { ProjectResourceService } from 'src/app/Services/projectResourceService';
import { convertToDate, dateFormatValidator } from 'src/app/utils/dateFormatValidator';

@Component({
  standalone: true,
  selector: 'project-route',
  templateUrl: './project-resources.component.html',
  styleUrls: ['./project-resources.component.scss'],
  imports: [RouterLink, ReactiveFormsModule],
})
export class ProjectResources implements OnInit {
  data: Array<projectResourceModel> = [];
  projectId: string = ';';
  resourcesForms: FormGroup = new FormGroup({
    resourcesitems: new FormGroup({
      name: new FormControl(''),
      role: new FormControl(''),
      startDate: new FormControl(''),
      endDate: new FormControl(''),
      allocationPercentage: new FormControl(''),
    }),
  });
  unauthorizedPerson: boolean = true;
  displayedColumns = ['Name','Role', 'AllocationPercentage', 'StartDate', 'EndDate', 'Comment'];

  constructor(
    private fb: FormBuilder,
    private projectResourceService: ProjectResourceService,
    private route: ActivatedRoute
  ) {
    this.projectId = this.route.snapshot.pathFromRoot[1].params['id'];
  }

  ngOnInit(): void {
    this.projectResourceService.getAllProjects().subscribe(
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
          alert("Error while fetching data");
        }
      }
    );
  }

  addExistingData(data: any): void {
    let existingData: any = [];
    data.forEach((element: any) => {
      existingData.push(this.existingDataFormGroup(element));
    });
    this.resourcesForms = this.fb.group({
      resourcesitems:
        existingData.length == 0 ? this.fb.array([this.createFormGroup()]) : this.fb.array(existingData),
    });
  }

  createFormGroup(): FormGroup {
    return this.fb.group({
      id: [''],
      name: ['', Validators.required],
      role: ['', Validators.required],
      startDate: ['', [Validators.required,dateFormatValidator()]],
      endDate: ['', [Validators.required,dateFormatValidator()]],
      allocationPercentage: ['', Validators.required],
    });
  }

  existingDataFormGroup(e: any): FormGroup {
    return this.fb.group({
      id: [e.id],
      name: [e.name, Validators.required],
      role: [e.role, Validators.required],
      startDate: [convertToDate(e.start), [Validators.required,dateFormatValidator()]],
      endDate: [convertToDate(e.end), [Validators.required,dateFormatValidator()]],
      allocationPercentage: [e.allocationPercentage, Validators.required],
    });
  }
  resourcesitems(): FormArray {
    return this.resourcesForms.get('resourcesitems') as FormArray;
  }

  addRow(): void {
    const approveteamArray = this.resourcesForms.get('resourcesitems') as FormArray;
    approveteamArray.push(this.createFormGroup());
  }

  removeRow(index: number): void {
    const approveteamArray = this.resourcesForms.get('resourcesitems') as FormArray;
    const controlAtIndex = approveteamArray.at(index);
    // console.log(controlAtIndex.value.id);
    this.projectResourceService.deleteProject(controlAtIndex.value.id).subscribe(
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
    if (this.resourcesForms.valid) {
      this.resourcesForms.value.resourcesitems.map(e => {
        const modelDate: projectResourceModel = {
          projectId: this.projectId,
          name: e.name,
          allocationPercentage: e.allocationPercentage,
          start: e.startDate,
          end: e.endDate,
          role: e.role,
        };

        console.log("id ",e.id);
        if (e.id == '') {
          console.log
          this.projectResourceService.createProject(modelDate).subscribe(
            data => {
              console.log(data);
            },
            error => {
              console.error('erorr');
            }
          );
        } else {
          this.projectResourceService.updateProject(e.id, modelDate).subscribe(
            data => {
              console.log(data);
            },
            error => {
              console.error('erorr');
            }
          );
        }
      });
    }
  }
}

// add for new
// update for exsting
// const dateString = "04/03/2024";
// const parts = dateString.split('/');
// const formattedDate = new Date(`${parts[2]}-${parts[1]}-${parts[0]}T00:00:00.000Z`).toISOString();

// console.log(formattedDate);
