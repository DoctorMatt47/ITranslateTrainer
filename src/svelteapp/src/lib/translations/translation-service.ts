import { TranslationApi } from "$lib/translations/translation-api";

export type TranslationStateItem = {
  id: number;
  originText: TextStateItem;
  translationText: TextStateItem;
}

export type TextStateItem = {
  id: number;
  value: string;
  language: string;
}

export type AddTranslationRequest = {
  originText: Omit<TextStateItem, "id">;
  translationText: Omit<TextStateItem, "id">;
}

export class TranslationService {
  translations: readonly TranslationStateItem[] = $state.frozen([]);

  private api: TranslationApi = new TranslationApi();

  async addTranslation(request: AddTranslationRequest): Promise<void> {
    const translation = await this.api.putTranslation(request);
    this.translations = [translation, ...this.translations];
  }

  async deleteTranslation(id: number): Promise<void> {
    await this.api.deleteTranslation(id);
    this.translations = this.translations.filter((translation) => translation.id !== id);
  }

  async fetchTranslations(): Promise<TranslationStateItem[]> {
    const translations = await this.api.getTranslations();
    this.translations = translations;
    return translations;
  }

  async importTranslationsSheet(file: File): Promise<void> {
    const response = await this.api.importTranslationSheet(file!);

    const addedTranslations = response
      .filter(translationOrError => !("errorMessage" in translationOrError))
      .map(translationOrError => translationOrError as TranslationStateItem);

    this.translations = [...this.translations, ...addedTranslations];
  }
}
