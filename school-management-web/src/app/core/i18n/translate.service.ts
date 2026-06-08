import { Injectable, signal } from '@angular/core';
import { Language, TRANSLATIONS } from './translations';

const STORAGE_KEY = 'sms_lang';

@Injectable({ providedIn: 'root' })
export class TranslateService {
  readonly lang = signal<Language>(this.loadLanguage());

  get(key: string, params?: Record<string, string | number>): string {
    const dictionary = TRANSLATIONS[this.lang()];
    let value = dictionary[key] ?? TRANSLATIONS.en[key] ?? key;

    if (params) {
      for (const [name, paramValue] of Object.entries(params)) {
        value = value.replace(`{{${name}}}`, String(paramValue));
      }
    }

    return value;
  }

  setLanguage(language: Language): void {
    this.lang.set(language);
    localStorage.setItem(STORAGE_KEY, language);
    this.applyDocumentLanguage(language);
  }

  toggleLanguage(): void {
    this.setLanguage(this.lang() === 'ar' ? 'en' : 'ar');
  }

  init(): void {
    this.applyDocumentLanguage(this.lang());
  }

  roleKey(role: string | null): string {
    return role ? this.get(`roles.${role}`) : '';
  }

  attendanceKey(status: string): string {
    return this.get(`attendance.${status}`);
  }

  private applyDocumentLanguage(language: Language): void {
    const html = document.documentElement;
    html.lang = language;
    html.dir = language === 'ar' ? 'rtl' : 'ltr';
    document.title = this.get('app.title');
  }

  private loadLanguage(): Language {
    const stored = localStorage.getItem(STORAGE_KEY);
    return stored === 'en' || stored === 'ar' ? stored : 'ar';
  }
}
