import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MoviesPagedListComponent } from './movies-paged-list.component';

describe('MoviesPagedListComponent', () => {
  let component: MoviesPagedListComponent;
  let fixture: ComponentFixture<MoviesPagedListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MoviesPagedListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MoviesPagedListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
