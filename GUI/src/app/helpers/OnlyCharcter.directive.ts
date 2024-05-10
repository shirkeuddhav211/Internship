import { Directive, ElementRef, HostListener, Input, Renderer2 } from '@angular/core';

@Directive({
  selector: '[CharactersOnlyDirective]'
})
export class CharactersOnlyDirective {
  constructor(private el: ElementRef, private renderer: Renderer2) {}

  // Change the input property name to avoid conflicts with the directive's name.
  @Input() set CharactersOnlyDirective(value: any) {}

  @HostListener('keydown', ['$event']) onKeyDown(event: KeyboardEvent) {
    // Ensure the event is valid and not blocked.
    if (!this.validateCharacter(event)) {
      event.preventDefault();
    }
  }

  validateCharacter(event: KeyboardEvent): boolean {
    const code = event.keyCode || 0;
    if (code === 8) {
      return true; // Allow backspace (code 8)
    } else if ((code >= 65 && code <= 90) || (code >= 97 && code <= 122) || event.ctrlKey) {
      return true; // Allow A-Z, a-z, and Ctrl key combinations
    } else {
      return false; // Block all other characters
    }
  }

  @HostListener('paste', ['$event']) onPaste(event: ClipboardEvent) {
    // Prevent pasting non-alphabetic characters and spaces
    this.validateFields(event);
  }

  validateFields(event: ClipboardEvent) {
    const pastedText = event.clipboardData?.getData('text/plain');
    if (pastedText) {
      // Remove non-alphabetic characters and spaces from the pasted text
      const sanitizedText = pastedText.replace(/[^a-zA-Z ]/g, '').replace(/\s/g, '');

      // Update the input field with the sanitized text using Renderer2
      this.renderer.setProperty(this.el.nativeElement, 'value', sanitizedText);
      event.preventDefault();
    }
  }
}
