import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
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
  imports: [CommonModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule],
  templateUrl: './entity-dialog.component.html',
  styleUrls: ['./entity-dialog.component.scss']
})
export class EntityDialogComponent {
  form: FormGroup;
  entityLabel = '';

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<EntityDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: EntityDialogData
  ) {
    this.entityLabel = this.data.entity;
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
      return;
    }
    const payload = this.form.value;
    this.dialogRef.close({ action: this.data.mode === 'create' ? 'create' : 'update', payload });
  }

  cancel() {
    this.dialogRef.close({ action: 'cancel' });
  }
}
