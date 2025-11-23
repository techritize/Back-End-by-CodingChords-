import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmitContent } from './submit-content';

describe('SubmitContent', () => {
  let component: SubmitContent;
  let fixture: ComponentFixture<SubmitContent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SubmitContent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SubmitContent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
