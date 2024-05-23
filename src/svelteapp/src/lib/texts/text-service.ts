import { TextApi } from "$lib/texts/text-api";

export type TextStateItem = {
  id: number;
  value: string;
  language: string;
};

export class TextService {
  private readonly api = new TextApi();

  async updateText(oldTextItem: TextStateItem, newText: string): Promise<void> {
    await this.api.putText(oldTextItem.id, { text: newText });
    oldTextItem.value = newText;
  }
}
