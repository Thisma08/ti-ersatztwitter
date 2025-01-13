import {Component, OnInit} from '@angular/core';
import {TweetService} from '../../../services/tweet.service';
import {Tweet} from '../../../classes/tweet';
import {DeleteTweetComponent} from '../delete-tweet/delete-tweet.component';
import {User} from '../../../classes/user';
import {UserService} from '../../../services/user.service';
import {LikeTweetComponent} from '../like-tweet/like-tweet.component';
import {LikeService} from '../../../services/like.service';

@Component({
  selector: 'app-tweet-list',
  imports: [
    DeleteTweetComponent,
    LikeTweetComponent
  ],
  standalone: true,
  templateUrl: './tweet-list.component.html',
  styleUrl: './tweet-list.component.css'
})
export class TweetListComponent implements OnInit {
  tweets: Tweet[] = [];
  users: User[] = [];
  likeCounts: Map<number, number> = new Map();

  constructor(private tweetService: TweetService, private userService: UserService, private likeService: LikeService) {
  }

  ngOnInit(): void {
    this.loadTweets();
    this.loadUsers();
  }

  private loadTweets() {
    this.tweetService.fetchAllTweets().subscribe((data: Tweet[]) => {
      this.tweets = data;
      this.loadVoteCounts();
    });
  }

  private loadUsers() {
    this.userService.fetchAllUsers().subscribe((data: User[]) => {
      this.users = data;
    });
  }

  private loadVoteCounts() {
    this.tweets.forEach(tweet => {
      this.getLikeCount(tweet.id);
    });
  }

  getLikeCount(tweetId: number | undefined): void {
    if (tweetId === undefined) return;

    this.likeService.getLikeCount(tweetId).subscribe({
      next: (response) => {
        const count = response.likeCount;
        this.likeCounts.set(tweetId, count);
      },
      error: (err) => {
        console.error('Error fetching like count', err);
      }
    });
  }

  onTweetDeleted(tweetToDelete: Tweet): void {
    this.tweets = this.tweets.filter(music => music.id !== tweetToDelete.id);
  }

  getUserPseudo(idUser: number): string {
    const user = this.users.find(u => u.id === idUser);
    return user ? user.pseudo : 'Unknown';
  }

  onTweetLiked($event: Tweet) {
    this.loadTweets();
  }
}
