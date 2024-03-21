import { ConfigStateService, isArray, isObject } from '@abp/ng.core';
import { NgFor, NgIf } from '@angular/common';
import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
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
import { ApprovedTeam } from 'src/app/Model/ApprovedTeamModel';
import { Phase } from 'src/app/Model/PhaseModel';
import { ApprovedTeamService } from 'src/app/Services/approvedTeamService';
import { PhaseService } from 'src/app/Services/phaseSerivce';

@Component({
  standalone: true,
  selector: 'project-route',
  templateUrl: './approved-team.component.html',
  styleUrls: ['./approved-team.component.scss'],
  imports: [RouterLink, ReactiveFormsModule],
})
export class ApprovedTeamComponent implements OnInit {
  data: Array<ApprovedTeam> = [];
  projectId: string = '';
  
  phaseId: {
    id: string;
    number: any;
  };
  forms: FormGroup = new FormGroup({
    formitem: new FormGroup({
      feedbacktype: new FormControl(''),
      receivedate: new FormControl(''),
      details: new FormControl(''),
      action: new FormControl(''),
      closure: new FormControl(''),
    }),
  });
  phaseForm: FormGroup = new FormGroup({
    selectedPhase: new FormControl(''),
  });

  unauthorizedPerson: boolean = true;
  displayedColumns = ['No. Of resources', 'Role', ' Availability%', 'Duration'];

  allPhases: Array<Phase> = [];
  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private approvedTeamService: ApprovedTeamService,
    private phaseService: PhaseService,
    private config:ConfigStateService
  ) {
    this.projectId = this.route.snapshot.pathFromRoot[1].params['id'];
  }

  ngOnInit(): void {
    this.initPhaseForm();
    this.phaseService.getAllItem().subscribe(
      data => {
        console.log('Projects:', data);
        // this.allPhases = data;
        this.allPhases = data.filter(phase => phase.projectId == this.projectId);
      },
      error => {
        console.error('Error fetching projects:', error);
      }
    );
    this.approvedTeamService.getAllItem().subscribe(
      data => {
        console.log('Projects:', data);
        this.data = data.filter(item => item.projectId == this.projectId);
      },
      error => {
        if (error.status == 403) {
          this.unauthorizedPerson = false;
          console.log(this.unauthorizedPerson);
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

  onSelectedPhaseChange(value: string): void {
    // console.log(value);
    this.addExistingData(value);
  }

  initPhaseForm(): void {
    this.phaseForm = this.fb.group({
      selectedPhase: ['', Validators.required],
    });
  }

  addExistingData(phaseId: string): void {
    let existingData: any = [];
    this.data.forEach((element: any) => {
      if (element.phaseId == phaseId) {
        existingData.push(this.existingFormGroup(element));
      }
    });
    this.forms = this.fb.group({
      formitem:
        existingData == 0 ? this.fb.array([this.createFormGroup()]) : this.fb.array(existingData),
    });
  }

  existingFormGroup(e: any): FormGroup {
    return this.fb.group({
      id: [e.id],
      phaseId: [e.phaseId],
      noofresources: [e.numberOfResources, Validators.required],
      role: [e.role, Validators.required],
      availability: [e.availabilityPercentage, Validators.required],
      duration: [e.duration, Validators.required],
    });
  }

  createFormGroup(): FormGroup {
    return this.fb.group({
      id: [''],
      phaseId: [''],
      noofresources: ['', Validators.required],
      role: ['', Validators.required],
      availability: ['', Validators.required],
      duration: ['', Validators.required],
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
    this.approvedTeamService.deleteItem(controlAtIndex.value.id).subscribe(
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
          const modelDate: ApprovedTeam = {
            phaseId: this.phaseForm.value.selectedPhase,
            projectId: this.projectId,
            numberOfResources: e.noofresources,
            role: e.role,
            availabilityPercentage: e.availability,
            duration: e.duration,
          };
          console.log(modelDate);

          if (e.id != '') {
            const data = await this.approvedTeamService.updateItem(e.id, modelDate).toPromise();
            console.log(data);
          } else {
            const data = await this.approvedTeamService.createItem(modelDate).toPromise();
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
