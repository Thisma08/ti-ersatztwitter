import { Routes } from '@angular/router';
import {TweetListComponent} from './components/tweet/tweet-list/tweet-list.component';
import {CreateTweetComponent} from './components/tweet/create-tweet/create-tweet.component';

export const routes: Routes = [
  {path: 'tweets', component: TweetListComponent},
  {path: 'write-tweet', component: CreateTweetComponent}
];
