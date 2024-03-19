import type { TranslationResponse } from "./services/translation-service";
import type { Writable } from "svelte/store";
import { writable } from "svelte/store";
import type { PutTestRequest } from "./services/test-service";

export const translationsStore: Writable<TranslationResponse[]> = writable([]);

export const testSettingsStore: Writable<PutTestRequest> = writable({
  fromLanguage: "english",
  toLanguage: "russian",
  optionCount: 10,
});

interface TestState {
  id: number;
  text: string;
  options: OptionState[];
}

interface OptionState {
  id: number;
  text: string;
  isChosen?: boolean;
  isCorrect?: boolean;
}

export const testStore: Writable<TestState | null> = writable(null!);
