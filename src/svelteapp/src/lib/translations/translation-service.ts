import type { TextApiResponse } from "$lib/common/services/text-service";
import { TranslationsApi } from "$lib/translations/translation-api";

type TranslationStateItem = {
  id: number;
  originText: TextApiResponse;
  translationText: TextApiResponse;
}

type TextStateItem = {
  id: number;
  value: string;
  language: string;
}

export class TranslationService {
  private state: TranslationStateItem[] = $state([]);
  private api: TranslationsApi = new TranslationsApi();

  get translations(): TranslationStateItem[] {
    return this.state;
  }

  async addTranslation(translation: TranslationStateItem): Promise<void> {
    await this.api.putTranslation(translation);
    this.state.push(translation);
  }

  async deleteTranslation(id: number): Promise<void> {
    await this.api.deleteTranslation(id);
    this.state = this.state.filter((translation) => translation.id !== id);
  }

  async fetchTranslations(): Promise<TranslationStateItem[]> {
    const translations = await this.api.getTranslations();
    this.state = translations;
    return translations;
  }

  async importTranslationsSheet(file: File): Promise<void> {
    const response = await this.api.importTranslationSheet(file!);

    const addedTranslations = response
      .filter(translationOrError => !("errorMessage" in translationOrError))
      .map(translationOrError => translationOrError as TranslationStateItem);

    this.state = [...this.state, ...addedTranslations];
  }
}
