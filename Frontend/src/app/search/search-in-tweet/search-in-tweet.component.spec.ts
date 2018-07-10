import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchInTweetComponent } from './search-in-tweet.component';

describe('SearchInTweetComponent', () => {
  let component: SearchInTweetComponent;
  let fixture: ComponentFixture<SearchInTweetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SearchInTweetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchInTweetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
