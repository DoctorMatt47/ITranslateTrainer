<script lang="ts">
  import AppVariant from "$lib/components/AppVariant.svelte";
  import { importSheet } from "$lib/services/translation-service.js";
  import { translationsStore } from "$lib/stores";
  import type { TranslationResponse } from "$lib/services/translation-service";

  async function importSheetClick(event: Event) {
    const input = event.currentTarget as HTMLInputElement;
    const file = input?.files?.item(0);

    const response = await importSheet(file!);
    const addedTranslations = response
      .filter(translationOrError => !("errorMessage" in translationOrError))
      .map(translationOrError => translationOrError as TranslationResponse);

    translationsStore.update(translations => {
      translations.push(...addedTranslations);
      return translations;
    });
  }
</script>

<label class="import">
  <AppVariant>
    <input accept=".xlsx" on:change={importSheetClick} style="display: none" type="file">
    Import
  </AppVariant>
</label>

<style>
    .import:hover {
        cursor: pointer;
    }
</style>
