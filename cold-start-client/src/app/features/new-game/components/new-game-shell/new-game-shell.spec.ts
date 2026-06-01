import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewGameShell } from './new-game-shell';

describe('NewGameShell', () => {
  let component: NewGameShell;
  let fixture: ComponentFixture<NewGameShell>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NewGameShell]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NewGameShell);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
