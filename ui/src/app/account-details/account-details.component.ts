import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { catchError } from 'rxjs/operators';

import { NumberInputDialogComponent } from '../number-input-dialog/number-input-dialog.component';
import { AccountService } from '../account.service';
import { of } from 'rxjs';

@Component({
  selector: 'app-account-details',
  templateUrl: './account-details.component.html',
  styleUrls: ['./account-details.component.css']
})
export class AccountDetailsComponent implements OnInit {
  account: any;

  constructor(
    private accountService: AccountService,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.getAccountDetails();
  }

  getAccountDetails(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.accountService.getAccount(id).subscribe((account: any) => this.account = account);
    }
  }

  withdraw(accountId: string): void {
    const dialogRef = this.dialog.open(NumberInputDialogComponent, {
      width: '250px',
      data: { title: 'Withdrawal', label: 'Enter amount to withdraw:', value: 0 }
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result) {
        this.accountService.withdraw(accountId, result).pipe(
          catchError(error => {
            this.snackBar.open(`${error.error}`, 'Close', { duration: 5000, verticalPosition: 'top'});
            return of(null);
          })).subscribe(() => {
          this.getAccountDetails();
        })
      }
    });
  }

  deposit(accountId: string): void {
    const dialogRef = this.dialog.open(NumberInputDialogComponent, {
      width: '250px',
      data: { title: 'Deposit', label: 'Enter amount to deposit:', value: 0 }
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result) {
        this.accountService.deposit(accountId, result).pipe(
          catchError(error => {
            this.snackBar.open(`${error.error}`, 'Close', { duration: 5000, verticalPosition: 'top'});
            return of(null);
          })).subscribe(() => {
          this.getAccountDetails();
        })
      }
    });
  }
}
