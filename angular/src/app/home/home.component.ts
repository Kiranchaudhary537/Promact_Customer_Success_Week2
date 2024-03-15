import { AuthService, ConfigStateService } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { RouterOutlet } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProjectService } from '../Services/projectService';
import { Project } from '../Model/ProjectModel';

@Component({
  standalone: true,
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  imports: [SharedModule, RouterOutlet, ReactiveFormsModule],
})
export class HomeComponent {
  isModalOpen: boolean;
  inProgress: boolean;
  currentStage: number = 1;
  clientForm = this.fb.group({
    clientName: ['', [Validators.required]],
    clientEmail: ['', [Validators.required, Validators.email]],
  });
  managerForm = this.fb.group({
    manager: ['', [Validators.required]],
  });
  managers = [
    { id: 1, name: 'Dipa' },
    { id: 2, name: 'Firoza' },
  ];
  projectForm = this.fb.group({
    name: ['', [Validators.required]],
    description: ['', [Validators.required]],
  });

  constructor(
    private authService: AuthService,
    private config: ConfigStateService,
    private  projectService:ProjectService,
    private fb: FormBuilder
  ) {
  }
  get hasLoggedIn(): boolean {
    return this.authService.isAuthenticated;
  }


  ngOnInit(): void {
    const currentUser = this.config.getOne("currentUser");
    console.log(currentUser);
  }
  login() {
    this.authService.navigateToLogin();
    
  }
  nextStage() {
    this.currentStage++;
  }

  prevStage() {
    this.currentStage--;
  }
  ngSubmit() {
    console.log(this.projectForm.value);
    console.log(this.managerForm.value);
    console.log(this.clientForm.value);
    if (this.projectForm.invalid) return;
    if (this.currentStage === 1 && this.clientForm.invalid) return;
    if (this.currentStage === 2 && this.managerForm.invalid) return;
    console.log(this.projectForm.value);
    
    const modalData:Project={
      name:this.projectForm.value.name,
      description:this.projectForm.value.description,
      projectManager:this.managerForm.value.manager,
      member:"0",
      status:"inprogress"
    }
    this.projectService.createProject(modalData).subscribe
    ((data)=>{
      this.isModalOpen = false;
      console.log(data);
    })
  }
}
