import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LikeTweetComponent } from './like-tweet.component';

describe('LikeTweetComponent', () => {
  let component: LikeTweetComponent;
  let fixture: ComponentFixture<LikeTweetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LikeTweetComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LikeTweetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
