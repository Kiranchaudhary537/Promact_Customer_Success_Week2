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
import { take } from 'rxjs';
import { ScopeStakeService } from 'src/app/Services/scopeandstakeService';
@Component({
  standalone: true,
  selector: 'project-route',
  templateUrl: './scopestake.component.html',
  styleUrls: ['./scopestake.component.scss'],
  imports: [RouterLink, ReactiveFormsModule],
})
export class ScopeStakeComponent implements OnInit {
  projectId: string = '';
  form: FormGroup = new FormGroup({
    stake: new FormControl(''),
    scope: new FormControl(''),
  });

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private scopestakeService: ScopeStakeService
  ) {
    this.projectId = this.route.snapshot.pathFromRoot[1].params['id'];
  }

  ngOnInit(): void {
    this.fetchValues();
  }

  fetchValues() {
    this.scopestakeService.getItems().subscribe(
      (data: any) => {
        const d = data.filter(e => e.projectId == this.projectId);
        if (d.length == 0) {
          this.addNewDataToForm();
        } else {
          this.initializeFrom(d[0]);
        }
      },
      error => {
        this.addNewDataToForm();
        console.error('Error fetching project details:', error);
      }
    );
  }

  initializeFrom(e): void {
    this.form = this.fb.group({
      id: [e?.id],
      stake: [e?.stake, Validators.required],
      scope: [e?.scope, Validators.required],
    });
  }

  addNewDataToForm(): void {
    this.form = this.fb.group({
      id: [''],
      stake: ['', Validators.required],
      scope: ['', Validators.required],
    });
  }

  async handleSubmit(modelData: any): Promise<any> {
    console.log(modelData);
    try {
      if (modelData.id != '') {
        return await this.scopestakeService.updateItem(modelData.id,modelData).toPromise();
      } else {
        return await this.scopestakeService.createItems(modelData).toPromise();
      }
    } catch (error) {
      console.error('Error:', error);
    }
  }

  onSubmit(): void {
    if (!this.form.valid) {
      alert('Form is not valid');
      return;
    }
    console.log(this.projectId);
    const modelData: any = {
      id:this.form.value.id,
      projectId: this.projectId,
      scope: this.form.value.scope,
      stake: this.form.value.stake,
    };

    this.handleSubmit(modelData).then(data => {
      console.log(data);
    });
  }
}
