import type {TranslationResponse} from "./services/translation-service";
import type {Writable} from "svelte/store";
import {writable} from "svelte/store";
import type {PutTestRequest, TestResponse} from "./services/test-service";

export const translationsStore: Writable<TranslationResponse[]> = writable([]);

export const testSettingsStore: Writable<PutTestRequest> = writable({
    from: "english",
    to: "russian",
    optionCount: 10,
});

export const testStore: Writable<TestResponse | null> = writable(null!);
