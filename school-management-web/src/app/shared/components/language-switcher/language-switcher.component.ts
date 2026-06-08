import { Component, inject } from '@angular/core';
import { TranslateService } from '../../../core/i18n/translate.service';
import { Language } from '../../../core/i18n/translations';
import { TranslatePipe } from '../../../core/i18n/translate.pipe';

@Component({
  selector: 'app-language-switcher',
  imports: [TranslatePipe],
  template: `
    <div class="lang-switcher">
      @for (option of options; track option) {
        <button
          type="button"
          class="lang-btn"
          [class.active]="lang() === option"
          (click)="setLanguage(option)"
        >
          {{ 'lang.' + option | translate }}
        </button>
      }
    </div>
  `,
  styles: `
    .lang-switcher {
      display: flex;
      gap: 0.35rem;
      background: rgba(0, 0, 0, 0.06);
      padding: 0.25rem;
      border-radius: 10px;
    }

    .lang-btn {
      border: none;
      background: transparent;
      padding: 0.4rem 0.75rem;
      border-radius: 8px;
      cursor: pointer;
      font-family: inherit;
      font-size: 0.85rem;
      font-weight: 600;
      color: #4b5563;
      transition: all 0.2s;
    }

    .lang-btn.active {
      background: #fff;
      color: #1e3a5f;
      box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
    }
  `
})
export class LanguageSwitcherComponent {
  private readonly translate = inject(TranslateService);

  readonly lang = this.translate.lang;
  readonly options: Language[] = ['ar', 'en'];

  setLanguage(language: Language): void {
    this.translate.setLanguage(language);
  }
}
