import { Directive, Input, HostListener, ElementRef } from '@angular/core';

@Directive({
  selector: '[OnlynumbernodotDirective]'
})
export class OnlynumbernodotDirective {

  constructor(private el: ElementRef) { }

  @Input() OnlynumbernodotDirective: any;

  @HostListener('paste', ['$event']) blockPaste(e: KeyboardEvent) {
    e.preventDefault();
  }

  @HostListener('keydown', ['$event']) onKeyDown(event: KeyboardEvent) {
    let e = <KeyboardEvent>event;
    if (this.OnlynumbernodotDirective) {
      if (["Delete", "Backspace", "Tab", "Escape", "Enter", "Period", "NumpadDecimal"].indexOf(e.code) !== -1 ||
        // Allow: Ctrl+A
        (e.code == "KeyA" && e.ctrlKey === true) ||
        // Allow: Ctrl+C
        (e.code == "KeyC" && e.ctrlKey === true) ||
        // Allow: Ctrl+X
        (e.code == "KeyX" && e.ctrlKey === true) ||
        // Allow: home, end, left, right
        (e.code >= "End" && e.code <= "ArrowRight")) {

        if (["Period", "NumpadDecimal"].indexOf(e.code) !== -1 && this.OnlynumbernodotDirective.split('.').length === 2) {
          e.preventDefault();
        }
        // let it happen, don't do anything
        return;
      }
      // Ensure that it is a number and stop the keypress

      if ((e.shiftKey || (e.code < "Digit0" || e.code > "Digit9")) && (e.code < "Numpad0" || e.code > "Numpad9")) {
        e.preventDefault();
      }


    }
  }
}










