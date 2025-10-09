import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListsPosts } from './lists-posts';

describe('ListsPosts', () => {
  let component: ListsPosts;
  let fixture: ComponentFixture<ListsPosts>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListsPosts]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListsPosts);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
