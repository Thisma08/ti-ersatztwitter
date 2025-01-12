import {Component, EventEmitter, Input, Output} from '@angular/core';
import {TweetService} from '../../../services/tweet.service';
import { Tweet } from '../../../classes/tweet';
import {Like} from '../../../classes/like';
import {LikeService} from '../../../services/like.service';

@Component({
  selector: 'app-like-tweet',
  standalone: true,
  imports: [],
  templateUrl: './like-tweet.component.html',
  styleUrl: './like-tweet.component.css'
})
export class LikeTweetComponent {
  errorMessage: string = '';
  connectedUserId: number | null = null;

  @Input() tweet!: Tweet;
  @Output() likedTweet = new EventEmitter<Tweet>();

  constructor(private likeService: LikeService) {
    this.connectedUserId = this.getConnectedUserId();
  }

  private getConnectedUserId(): number | null {
    const cookieValue = document.cookie
      .split('; ')
      .find((row) => row.startsWith('ConnectedUserId='))
      ?.split('=')[1];

    return cookieValue ? parseInt(cookieValue, 10) : null;
  }

  likeTweet(): void {
    this.likeService.createLike(this.connectedUserId, this.tweet.id).subscribe({
      next: () => {
        this.likedTweet.emit(this.tweet);
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
