import {Component, OnInit} from '@angular/core';
import {User} from '../../../classes/user';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {UserService} from '../../../services/user.service';

@Component({
  selector: 'app-connect',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './connect.component.html',
  styleUrl: './connect.component.css'
})
export class ConnectComponent implements OnInit {
  users: User[] = [];
  connectForm: FormGroup;
  successMessage: string = '';
  errorMessage: string = '';

  constructor(private fb: FormBuilder, private userService: UserService) {
    this.connectForm = this.fb.group({
      userId: ''
    });
  }

  ngOnInit() {
    this.loadUsers();
  }

  loadUsers(): void {
    this.userService.fetchAllUsers().subscribe((data: User[]) => {
      this.users = data;
    });
  }

  onSubmit(): void {
    if (this.connectForm.valid) {
      const formValue = this.connectForm.value;
      const userId = Number(formValue.userId);
      this.userService.connectUser(userId).subscribe({
        next: () => {
          this.successMessage = 'User connected successfully!';
          this.connectForm.reset();
          setTimeout(() => {
            this.successMessage = '';
          }, 5000);
        },
        error: (err) => {
          this.errorMessage = err?.message || 'An unexpected error occurred.';
          setTimeout(() => {
            this.errorMessage = '';
          }, 5000);
        },
      });
    }
  }
}
