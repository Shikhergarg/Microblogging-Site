import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchInUserComponent } from './search-in-user.component';

describe('SearchInUserComponent', () => {
  let component: SearchInUserComponent;
  let fixture: ComponentFixture<SearchInUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SearchInUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchInUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
