import type { TranslationResponse } from "./services/translation-service";
import type { Writable } from "svelte/store";
import { writable } from "svelte/store";

export const translationsStore: Writable<TranslationResponse[]> = writable([]);
