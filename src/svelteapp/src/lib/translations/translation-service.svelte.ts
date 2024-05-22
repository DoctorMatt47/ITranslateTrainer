import { type PutTranslationApiRequest, TranslationApi } from "$lib/translations/translation-api";
import type { TextStateItem } from "$lib/texts/text-service";

export type TranslationStateItem = {
  id: number;
  originText: TextStateItem;
  translationText: TextStateItem;
};

export class TranslationService {
  translations: readonly TranslationStateItem[] = $state.frozen([]);

  private api: TranslationApi = new TranslationApi();

  async addTranslation(request: PutTranslationApiRequest): Promise<void> {
    const translation = await this.api.putTranslation(request);
    this.translations = [translation, ...this.translations];
  }

  async deleteTranslation(id: number): Promise<void> {
    await this.api.deleteTranslation(id);
    this.translations = this.translations.filter((translation) => translation.id !== id);
  }

  async fetchTranslations(): Promise<readonly TranslationStateItem[]> {
    this.translations = await this.api.getTranslations();
    return this.translations;
  }

  async importTranslationsSheet(file: File): Promise<void> {
    const response = await this.api.importTranslationSheet(file);

    const addedTranslations = response
      .filter((translationOrError) => !("errorMessage" in translationOrError))
      .map((translationOrError) => translationOrError as TranslationStateItem);

    this.translations = [...this.translations, ...addedTranslations];
  }
}
