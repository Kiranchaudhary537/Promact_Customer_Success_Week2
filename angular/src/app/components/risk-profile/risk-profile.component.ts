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
import { RiskProfileModel } from 'src/app/Model/Models';
import { MeetingMinute } from 'src/app/Model/MomModel';
import { MomService } from 'src/app/Services/momService';
import { RiskProfileService } from 'src/app/Services/riskProfileService';
import { convertToDate, dateFormatValidator } from 'src/app/utils/dateFormatValidator';

@Component({
  standalone: true,
  selector: 'project-route',
  templateUrl: './risk-profile.component.html',
  styleUrls: ['./risk-profile.component.scss'],
  imports: [RouterLink, ReactiveFormsModule],
})
export class RiskProfileComponent implements OnInit {
  data: Array<RiskProfileModel> = [];
  projectId: string = '';

  forms: FormGroup = new FormGroup({
    formitem: new FormGroup({
      riskType: new FormControl(''),
      severity: new FormControl(''),
      impact: new FormControl(''),
      description: new FormControl(''),
      status: new FormControl(''),
      closureDate: new FormControl('')
    }),
  });
  unauthorizedPerson: boolean = true;
  displayedColumns = ['RiskType', 'Severity', 'Impact',"Description","Status","Closure Date"];
  riskType = ['Financial', 'Operational', 'Technical', 'HumanResource',
  'External', 'Legal', 'Reputational', 'Strategic'];
  impactType = ['Low', 'Medium', 'High'];
  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private riskProfileService: RiskProfileService
  ) {
    this.projectId = this.route.snapshot.pathFromRoot[1].params['id'];
  }

  ngOnInit(): void {
    this.riskProfileService.getAllItem().subscribe(
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
      riskType: ['', Validators.required],
      severity: ['', Validators.required],
      impact: ['', Validators.required],
      description: ['', Validators.required],
      status: ['', Validators.required],
      closureDate: ['',[Validators.required,dateFormatValidator()]]
    });
  }

  existingDataFormGroup(e: any): FormGroup {
    return this.fb.group({
      id: [e.id],
      riskType: [this.riskType[e.riskType], Validators.required],
      severity: [this.impactType[e.severity], Validators.required],
      impact: [this.impactType[e.impact], Validators.required],
      description: [e.description, Validators.required],
      status: [e.status, Validators.required],
      closureDate: [convertToDate(e.closureDate), [Validators.required,dateFormatValidator()]],
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
    this.riskProfileService.deleteItem(controlAtIndex.value.id).subscribe(
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
          const modelDate: RiskProfileModel = {
            projectId: this.projectId,
            riskType: e.riskType,
            severity: e.severity,
            impact: e.impact,
            description:e.description,
            status:e.status,
            closureDate:e.closureDate
          };
          console.log(e);
          if (e.id != '') {
            const data = await this.riskProfileService.updateItem(e.id, modelDate).toPromise();
            console.log(data);
          } else {
            const data = await this.riskProfileService.createItem(modelDate).toPromise();
            console.log(data);
          }
        } catch (error) {
          console.error('Error:', error);
        }
      });
    }
  }
}
