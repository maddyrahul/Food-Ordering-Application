import { Component, OnInit } from '@angular/core';
import { User } from '../../models/User';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrl: './edit-user.component.css'
})
export class EditUserComponent implements OnInit {
  userId: string='';
  user: User | undefined;
  editUserForm!: FormGroup;

  constructor(
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.userId = this.route.snapshot.paramMap.get('userId')!;
    console.log("userid", this.userId)
    this.loadUser();
    this.initForm();
  }

  loadUser(): void {
    this.userService.getUserById(this.userId).subscribe((user) => {
      this.user = user;
      console.log(this.user)
      this.editUserForm.patchValue({
        email: user.email,
        username: user.userName,
        isActive: user.isActive,
      });
    });
  }

  initForm(): void {
    this.editUserForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      username: ['', [Validators.required]],
      isActive: [true],
    });
  }

  onSubmit(): void {
    if (this.editUserForm.valid && this.user) {
      const { email, username, isActive } = this.editUserForm.value;
      this.userService.updateUser(this.user.id, email, username, isActive).subscribe(
        (response) => {
          alert('User updated successfully');
          this.router.navigate(['/users']);
        },
        (error) => {
          alert('Failed to update user');
        }
      );
    }
  }
}
