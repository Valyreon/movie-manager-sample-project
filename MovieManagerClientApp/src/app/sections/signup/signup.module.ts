import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SignupFormComponent } from './components/signup-form/signup-form.component';
import { AngularMaterialModule } from 'src/app/material.module';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [SignupFormComponent],
  imports: [
    CommonModule,
    AngularMaterialModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
  ],
})
export class SignupModule {}
