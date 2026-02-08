import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

export interface EntityDialogData {
  mode: 'create' | 'edit';
  entity: 'post' | 'category' | 'tag';
  item?: any;
}

@Component({
  selector: 'app-entity-dialog',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './entity-dialog.component.html',
  styleUrl: './entity-dialog.component.scss'
})
export class EntityDialogComponent {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialogRef<EntityDialogComponent>);
  public data = inject<EntityDialogData>(MAT_DIALOG_DATA);

  form: FormGroup;
  entityLabel = this.data.entity;

  constructor() {
    this.form = this.buildForm();
    
    if (this.data.item) {
      this.form.patchValue(this.data.item);
    }
  }

  buildForm(): FormGroup {
    switch (this.data.entity) {
      case 'post':
        return this.fb.group({
          id: [''],
          title: ['', Validators.required],
          content: [''],
          categoryId: ['']
        });
      case 'category':
        return this.fb.group({
          id: [''],
          name: ['', Validators.required],
          description: ['']
        });
      case 'tag':
        return this.fb.group({
          id: [''],
          name: ['', Validators.required]
        });
      default:
        return this.fb.group({});
    }
  }

  save() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const payload = this.form.value;
    this.dialogRef.close({ 
      action: this.data.mode === 'create' ? 'create' : 'update', 
      payload 
    });
  }

  cancel() {
    this.dialogRef.close({ action: 'cancel' });
  }
}