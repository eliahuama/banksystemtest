import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface DialogData {
  title: string;
  label: string;
  value: number;
}

@Component({
  selector: 'app-number-input-dialog',
  templateUrl: './number-input-dialog.component.html',
})
export class NumberInputDialogComponent {
  public dialogData: DialogData;

  constructor(
    public dialogRef: MatDialogRef<NumberInputDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {
      this.dialogData = data ? data : {
        title: 'Enter value',
        label: 'Value',
        value: 0
      };
    }

  ngOnInit(): void {
    if (this.dialogData.value === undefined) {
      this.dialogData.value = 0;
    }
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }

  onConfirmClick(): void {
    this.dialogRef.close(this.dialogData.value);
  }
}
