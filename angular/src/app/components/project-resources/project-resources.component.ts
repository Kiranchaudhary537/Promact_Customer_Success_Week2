import { ConfigStateService } from '@abp/ng.core';
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
  projectId: string = '';
  resourcesForms: FormGroup = new FormGroup({
    resourcesitems: new FormGroup({
      name: new FormControl(''),
      role: new FormControl(''),
      startDate: new FormControl(''),
      endDate: new FormControl(''),
      allocationPercentage: new FormControl(''),
      comment: new FormControl(''),
    }),
  });
  unauthorizedPerson: boolean = false;
  displayedColumns = ['Name','Role', 'AllocationPercentage', 'StartDate', 'EndDate', 'Comment'];

  constructor(
    private fb: FormBuilder,
    private projectResourceService: ProjectResourceService,
    private route: ActivatedRoute,
    private config:ConfigStateService
  ) {
    this.projectId = this.route.snapshot.pathFromRoot[2].params['id'];
  }

  ngOnInit(): void {
    this.projectResourceService.getAllProjects().subscribe(
      data => {
        console.log('Projects:', data);
        this.data = data.filter(item => item?.projectId == this.projectId);
        this.addExistingData(data.filter(item => item?.projectId == this.projectId));
      },
      error => {
        this.addExistingData([]);
        if (error.status == 403) {
          console.warn('Unauthorized access (403):', error);
        } else {
          console.error('Error fetching projects:', error);
        }
      }
    );
    if(this.config.getOne('currentUser').roles[0]=="client" || this.config.getOne('currentUser').roles[0]=="auditor" ){
      this.unauthorizedPerson=true;
   }
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
      comment:['',Validators.required],
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
      comment:[e?.comment,Validators.required],
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
    const getConfirmation=window.confirm("Do you want to delte");
    if(getConfirmation==false){
      return;
    }
    const approveteamArray = this.resourcesForms.get('resourcesitems') as FormArray;
    const controlAtIndex = approveteamArray.at(index);
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
    
    if (this.resourcesForms.valid && this.unauthorizedPerson==false) {
      this.resourcesForms.value.resourcesitems.map(e => {
        const modelDate: projectResourceModel = {
          projectId: this.projectId,
          name: e.name,
          allocationPercentage: e.allocationPercentage,
          start: e.startDate,
          end: e.endDate,
          role: e.role,
          comment:e.comment
        };

        console.log("id ",e.id);
        if (e.id == '') {
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
    else{
       alert("Make sure you filled right value, check date formate");
    }
  }
}

