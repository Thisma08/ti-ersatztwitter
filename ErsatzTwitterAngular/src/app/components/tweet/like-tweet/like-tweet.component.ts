import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
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
export class LikeTweetComponent implements OnInit {
  errorMessage: string = '';
  connectedUserId: number | null = null;
  isLiked: boolean = false;

  @Input() tweet!: Tweet;
  @Output() likedTweet = new EventEmitter<Tweet>();

  constructor(private likeService: LikeService) {
    this.connectedUserId = this.getConnectedUserId();
  }

  ngOnInit(): void {
    if (this.connectedUserId) {
      this.checkIfLiked();
    }
  }

  private getConnectedUserId(): number | null {
    const cookieValue = document.cookie
      .split('; ')
      .find((row) => row.startsWith('ConnectedUserId='))?.split('=')[1];

    return cookieValue ? parseInt(cookieValue, 10) : null;
  }

  private checkIfLiked(): void {
    this.likeService.isLikedByUser(this.connectedUserId, this.tweet.id).subscribe({
      next: (response) => {
        this.isLiked = response.exists;
        console.log(this.isLiked);
      },
      error: (err) => {
        console.error('Error checking like status', err);
      }
    });
  }

  likeTweet(): void {
    if (this.isLiked) {
      this.likeService.removeLike(this.connectedUserId, this.tweet.id).subscribe({
        next: () => {
          this.isLiked = false;
          this.likedTweet.emit(this.tweet);
        },
        error: (err) => {
          this.errorMessage = 'An unexpected error occurred while removing like.';
          setTimeout(() => {
            this.errorMessage = '';
          }, 5000);
        }
      });
    } else {
      this.likeService.createLike(this.connectedUserId, this.tweet.id).subscribe({
        next: () => {
          this.isLiked = true;
          this.likedTweet.emit(this.tweet);
        },
        error: (err) => {
          if (err.error && err.error.message) {
            this.errorMessage = err.error.message;
          } else {
            this.errorMessage = 'An unexpected error occurred.';
          }
          setTimeout(() => {
            this.errorMessage = '';
          }, 5000);
        }
      });
    }
  }
}
