/// <reference path="../../../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { SidebarItemComponent } from './sidebar-item.component';

let component: SidebarItemComponent;
let fixture: ComponentFixture<SidebarItemComponent>;

describe('sidebar-item component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ SidebarItemComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(SidebarItemComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});