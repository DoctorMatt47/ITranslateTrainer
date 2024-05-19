import { TestApi } from "$lib/tests/test-api";

export type OptionStateItem = {
  id: number;
  text: string;
  isCorrect?: boolean;
  isSelected?: boolean;
};

export type TestStateItem = {
  id: number;
  text: string;
  answerTime: string | null;
  options: OptionStateItem[];
};

export type TestSettingsStateItem = {
  fromLanguage: string;
  toLanguage: string;
  optionCount: number;
};

export class TestService {
  test: TestStateItem | null = $state(null);

  settings: TestSettingsStateItem = $state({
    fromLanguage: "english",
    toLanguage: "russian",
    optionCount: 10,
  });

  private isAnswered = false;

  private api = new TestApi();

  async fetchTest(): Promise<void> {
    if (this.test && !this.isAnswered) {
      return;
    }

    this.test = await this.api.putTest(this.settings);
    this.isAnswered = false;
  }

  async answerOnTest(optionId: number): Promise<void> {
    if (!this.test || this.isAnswered) {
      return;
    }

    const { correctOptionId } = await this.api.answerOnTest(this.test.id, { optionId });

    this.test!.options.find((o) => o.id === optionId)!.isSelected = true;
    this.test!.options.find((o) => o.id === correctOptionId)!.isCorrect = true;

    this.isAnswered = true;
  }

  resetTest(): void {
    this.test = null;
    this.isAnswered = false;
  }
}
