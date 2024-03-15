import { AuthService, ConfigStateService } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { RouterOutlet } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProjectService } from '../Services/projectService';

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
    clientName: [null, [Validators.required]],
    clientEmail: [null, [Validators.required, Validators.email]],
  });
  managerForm = this.fb.group({
    manager: [null, [Validators.required]],
  });
  managers = [
    { id: 1, name: 'Dipa' },
    { id: 2, name: 'Firoza' },
  ];
  projectForm = this.fb.group({
    name: [null, [Validators.required]],
    description: [null, [Validators.required]],
  });

  constructor(
    private authService: AuthService,
    private config: ConfigStateService,
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
  save() {
    if (this.projectForm.invalid) return;
    if (this.currentStage === 1 && this.clientForm.invalid) return;
    if (this.currentStage === 2 && this.managerForm.invalid) return;
    console.log(this.projectForm.value);

  }
}
