import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Tweet} from '../../../classes/tweet';
import {TweetService} from '../../../services/tweet.service';

@Component({
  selector: 'app-delete-tweet',
  standalone: true,
  imports: [],
  templateUrl: './delete-tweet.component.html',
  styleUrl: './delete-tweet.component.css'
})
export class DeleteTweetComponent {
  @Input() tweet!: Tweet;
  @Output() deletedTweet = new EventEmitter<Tweet>();

  errorMessage: string = '';

  constructor(private tweetService: TweetService) {}

  deleteMusic(): void {
    this.tweetService.deleteTweet(this.tweet.id).subscribe({
      next: () => {
        this.deletedTweet.emit(this.tweet);
      },
      error: (err) => {
        if(err.error && err.error.message) {
          this.errorMessage = err.error.message;
        }
        else {
          this.errorMessage = 'An unexpected error occurred.';
        }
        setTimeout(() => {
          this.errorMessage = '';
        }, 5000);
      }
    });
  }
}
