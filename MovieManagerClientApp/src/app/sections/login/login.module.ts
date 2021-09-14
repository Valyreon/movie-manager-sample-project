import { ILoginResponse } from './interfaces/login-response';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { AngularMaterialModule } from 'src/app/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [LoginFormComponent],
  imports: [CommonModule, AngularMaterialModule, FormsModule, RouterModule, ReactiveFormsModule],
})
export class LoginModule {}
