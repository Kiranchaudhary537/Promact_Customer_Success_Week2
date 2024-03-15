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
import { SprintModel } from 'src/app/Model/Models';
import { Phase } from 'src/app/Model/PhaseModel';
import { PhaseService } from 'src/app/Services/phaseSerivce';
import { SprintService } from 'src/app/Services/sprintService';
import { convertToDate, dateFormatValidator } from 'src/app/utils/dateFormatValidator';

@Component({
  standalone: true,
  selector: 'project-route',
  templateUrl: './sprint.component.html',
  styleUrls: ['./sprint.component.scss'],
  imports: [RouterLink, ReactiveFormsModule],
})
export class SprintComponent implements OnInit {
  data: Array<SprintModel> = [];
  projectId: string = '';
  phaseId: {
    id: string;
    number: any;
  };
  forms: FormGroup = new FormGroup({
    formitem: new FormGroup({
      startDate: new FormControl(''),
      endDate: new FormControl(''),
      status: new FormControl(''),
      comments: new FormControl(''),
      goals: new FormControl(''),
      sprintNumber: new FormControl(''),
    }),
  });
  phaseForm: FormGroup = new FormGroup({
    selectedPhase: new FormControl(''),
  });
  unauthorizedPerson: boolean = true;
  displayedColumns = ['Start Date', 'End Date', 'status', 'Comments','Goals','Sprint Number'];

  allPhases: Array<Phase> = [];
  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private sprintService: SprintService,
    private phaseService: PhaseService
  ) {
    this.projectId = this.route.snapshot.pathFromRoot[1].params['id'];
  }

  ngOnInit(): void {
    this.initPhaseForm();
    this.phaseService.getAllItem().subscribe(
      data => {
        console.log('Projects:', data);
        this.allPhases = data;
      },
      error => {
        console.error('Error fetching projects:', error);
      }
    );
    this.sprintService.getAllItem().subscribe(
      data => {
        console.log('Projects:', data);
        this.data = data;
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
        existingData.push(this.existingDataFormGroup(element));
      }
    });
    this.forms = this.fb.group({
      formitem:
        existingData == 0 ? this.fb.array([this.createFormGroup()]) : this.fb.array(existingData),
    });
  }

  createFormGroup(): FormGroup {

    return this.fb.group({
      id: [''],
      phaseId: [''],
      startDate: ['', [Validators.required, dateFormatValidator()]],
      endDate: ['', [Validators.required, dateFormatValidator()]],
      status: ['', Validators.required],
      links: ['', Validators.required],
      comments: ['', Validators.required],
      goals: ['', Validators.required],
      sprintNumber: ['', Validators.required],
    });
  }

  existingDataFormGroup(e: any): FormGroup {
    return this.fb.group({
      id: [e.id],
      phaseId: [e.phaseId],
      startDate: [e.startDate, [Validators.required, dateFormatValidator()]],
      endDate: [e.endDate, [Validators.required, dateFormatValidator()]],
      status: [e.status, Validators.required],
      links: [e.links, Validators.required],
      comments: [e.comments, Validators.required],
      goals: [e.goals, Validators.required],
      sprintNumber: [e.sprintNumber, Validators.required],
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
    this.sprintService.deleteItem(controlAtIndex.value.id).subscribe(
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
          const modelDate: SprintModel = {
            projectId: this.projectId,
            phaseId:this.phaseForm.value.selectedPhase,
            startDate: e.startDate,
            endDate: e.endDate,
            status: e.status,
            comments: e.comments,
            goals: e.goals,
            sprintNumber: e.sprintNumber,
          };
          console.log(e);
          if (e.id != '') {
            const data = await this.sprintService.updateItem(e.id, modelDate).toPromise();
            console.log(data);
          } else {
            const data = await this.sprintService.createItem(modelDate).toPromise();
            console.log(data);
          }
        } catch (error) {
          console.error('Error:', error);
        }
      });
    }
  }
}

