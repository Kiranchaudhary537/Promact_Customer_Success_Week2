import { NgFor, NgIf } from '@angular/common';
import { Component, Input, NgModule, OnInit } from '@angular/core';
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
import { OverviewService } from 'src/app/Services/overviewService';

@Component({
  standalone: true,
  selector: 'project-route',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss'],
  imports: [RouterLink, ReactiveFormsModule, NgFor, NgIf],
})
export class OverviewComponent implements OnInit {
  form: FormGroup;
  projectId: string = '';

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private overviewService: OverviewService
  ) {
    this.projectId = this.route.snapshot.pathFromRoot[1].params['id'];
  }

  ngOnInit(): void {
    this.fetchValues();
  }
  fetchValues() {
    this.overviewService.getItems().subscribe(
      (data: any) => {
        const d = data.filter(e => e.projectId == this.projectId);
        if (d.length == 0) {
          this.addNewDataToForm();
        }
        this.addDataToForm(d[0]);
      },
      error => {
        this.addNewDataToForm();
        console.error('Error fetching project details:', error);
      }
    );
  }

  addDataToForm(e): void {
    console.log(e.brief);
    this.form = this.fb.group({
      id: [e?.id],
      brief: [e.brief, Validators.required],
      purpose: [e.purpose, Validators.required],
      goals: [e.goals, Validators.required],
      objectives: [e.objectives, Validators.required],
      budget: [e.budget, Validators.required],
    });
  }
  addNewDataToForm(): void {
    this.form = this.fb.group({
      id: [''],
      brief: ['', Validators.required],
      purpose: ['', Validators.required],
      goals: ['', Validators.required],
      objectives: ['', Validators.required],
      budget: ['', Validators.required],
    });
  }
  async handleSubmit(modelData: any): Promise<any> {
    console.log(modelData);
    try {
      if (modelData.id != '') {
        return await this.overviewService.updateItem(modelData.id,modelData).toPromise();
      } else {
        return await this.overviewService.createItem(modelData).toPromise();
      }
    } catch (error) {
      console.error('Error:', error);
    }
  }

  onSubmit(): void {
    if (!this.form.valid) {
      alert('Form in not valid');
      return;
    }
    console.log(this.form.value);
    const modelData: any = {
      id: this.form.value.id,
      projectId: this.projectId,
      brief: this.form.value.brief,
      purpose: this.form.value.purpose,
      goals: this.form.value.goals,
      objectives: this.form.value.objectives,
      budget: this.form.value.budget,
    };

    this.handleSubmit(modelData).then(data => {
      console.log(data);
    });
  }
}
