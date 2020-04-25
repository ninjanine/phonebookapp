import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PhoneBookAddEditComponent } from './phone-book-add-edit.component';

describe('PhoneBookAddEditComponent', () => {
  let component: PhoneBookAddEditComponent;
  let fixture: ComponentFixture<PhoneBookAddEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PhoneBookAddEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PhoneBookAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
