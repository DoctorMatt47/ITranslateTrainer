import type { Writable } from "svelte/store";
import { writable } from "svelte/store";

export const testSettingsStore: Writable<PutTestRequest> = writable({
  fromLanguage: "english",
  toLanguage: "russian",
  optionCount: 10,
});

type TestState = {
  id: number;
  text: string;
  options: OptionState[];
}

type OptionState
{
  id: number;
  text: string;
  isChosen?: boolean;
  isCorrect?: boolean;
}

export const testStore: Writable<TestState | null> = writable(null!);
