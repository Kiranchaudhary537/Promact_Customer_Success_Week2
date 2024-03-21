import { ConfigStateService } from '@abp/ng.core';
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
import { EscalationMatrixModel } from 'src/app/Model/Models';
import { MeetingMinute } from 'src/app/Model/MomModel';
import { EscalationMatrixService } from 'src/app/Services/escalationMatrixService';
@Component({
  standalone: true,
  selector: 'project-route',
  templateUrl: './escalation-matrix.component.html',
  styleUrls: ['./escalation-matrix.component.scss'],
  imports: [RouterLink, ReactiveFormsModule],
})
export class EscalationMatrixComponent implements OnInit {
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
  unauthorizedPerson: boolean = false;
  displayedColumns = ['Level', 'EscalationType'];
  levelType = ['Level1', 'Level2', 'Level3', 'Level4', 'Level5'];
  escalationType = ['Operational', 'Financial', 'Technical'];
  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private escalationMatrixService: EscalationMatrixService,
    private config: ConfigStateService
  ) {
    this.projectId = this.route.snapshot.pathFromRoot[2].params['id'];
  }

  ngOnInit(): void {
    this.escalationMatrixService.getAllItem().subscribe(
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
    if (
      this.config.getOne('currentUser').roles[0] == 'client' ||
      this.config.getOne('currentUser').roles[0] == 'auditor'
    ) {
      this.unauthorizedPerson = true;
    }
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
      level: ['', Validators.required],
      escalationType: ['', Validators.required],
    });
  }

  existingDataFormGroup(e: any): FormGroup {
    return this.fb.group({
      id: [e.id],
      level: [this.levelType[e.level], Validators.required],
      escalationType: [this.escalationType[e.escalationType], Validators.required],
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
    const getConfirmation = window.confirm('Do you want to delte');
    if (getConfirmation == false) {
      return;
    }
    const approveteamArray = this.forms.get('formitem') as FormArray;
    const controlAtIndex = approveteamArray.at(index);
    this.escalationMatrixService.deleteItem(controlAtIndex.value.id).subscribe(
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
    if (this.forms.valid && this.unauthorizedPerson==false) {
      this.forms.value.formitem.forEach(async e => {
        try {
          const modelDate: EscalationMatrixModel = {
            projectId: this.projectId,
            level: e.level,
            escalationType: e.escalationType,
          };
          console.log(e);
          if (e.id != '') {
            const data = await this.escalationMatrixService.updateItem(e.id, modelDate).toPromise();
            console.log(data);
          } else {
            const data = await this.escalationMatrixService.createItem(modelDate).toPromise();
            console.log(data);
          }
        } catch (error) {
          console.error('Error:', error);
        }
      });
    } else {
      alert('Make sure you filled right value');
    }
  }
}
