import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AccountService } from '../account.service';
import { CreateAccountDialogComponent } from '../create-account-dialog/create-account-dialog.component';
import { Router } from '@angular/router';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { MatTableDataSource } from '@angular/material/table';
import { Account } from '../models/account';

@Component({
  selector: 'app-account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.css']
})
export class AccountListComponent implements OnInit {
  accounts: any[];
  displayedColumns: string[] = ['id', 'balance', 'actions'];
  dataSource: MatTableDataSource<Account>;

  constructor(
    public dialog: MatDialog,
    private accountService: AccountService,
    private router: Router
  ) {
    this.accounts = [];
    this.dataSource = new MatTableDataSource<Account>([]);
  }

  ngOnInit(): void {
    this.getAccounts();
  }

  getAccounts(): void {
    this.accountService.getAccounts().subscribe((accounts: any) => {
      this.accounts = accounts;
      this.dataSource = new MatTableDataSource(accounts);
    });
  }

  openCreateAccountDialog(): void {
    const dialogRef = this.dialog.open(CreateAccountDialogComponent, {
      width: '250px',
      data: { balance: 100 }
    });

    dialogRef.afterClosed().subscribe(balance => {
      if(balance) {
        this.accountService.createAccount('', balance).subscribe(() => {
          this.getAccounts();
        });
      }
    });
  }

  deleteAccount(event: MouseEvent, accountId: string): void {
    event.stopPropagation(); // Stop event propagation
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: { message: 'Are you sure you want to delete this account?' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result) {
        this.accountService.deleteAccount(accountId).subscribe(() => {
          this.getAccounts();
        });
      }
    });
  }

  goToDetails(id: string) {
    this.router.navigate(['/account-details', id]);
  }
}
