import { Component } from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {Tweet} from '../../../classes/tweet';
import {TweetService} from '../../../services/tweet.service';

@Component({
  selector: 'app-create-tweet',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './create-tweet.component.html',
  styleUrl: './create-tweet.component.css'
})
export class CreateTweetComponent {
  writeTweetForm: FormGroup;
  successMessage: string = '';
  errorMessage: string = '';
  connectedUserId: number | null = null;

  constructor(private fb: FormBuilder, private tweetService: TweetService) {
    this.writeTweetForm = this.fb.group({
      content: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(140)]],
    });

    this.connectedUserId = this.getConnectedUserId();
  }

  private getConnectedUserId(): number | null {
    const cookieValue = document.cookie
      .split('; ')
      .find((row) => row.startsWith('ConnectedUserId='))
      ?.split('=')[1];

    return cookieValue ? parseInt(cookieValue, 10) : null;
  }

  onSubmit(): void {
    if (this.writeTweetForm.valid && this.connectedUserId !== null) {
      const tweet: Tweet = {
        ...this.writeTweetForm.value,
        userId: this.connectedUserId,
      };

      this.tweetService.createTweet(tweet).subscribe({
        next: (response) => {
          this.successMessage = 'Tweet has been created successfully!';
          this.writeTweetForm.reset();
          setTimeout(() => {
            this.successMessage = '';
          }, 5000);
        },
        error: (err) => {
          if (err.message) {
            this.errorMessage = err.message;
          } else {
            this.errorMessage = 'An unexpected error occurred.';
          }
          setTimeout(() => {
            this.errorMessage = '';
          }, 5000);
        },
      });
    } else {
      this.errorMessage = 'User not logged in or form is invalid.';
    }
  }
}

