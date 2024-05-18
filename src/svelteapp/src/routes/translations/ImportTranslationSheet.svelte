<script lang="ts">
  import AppVariant from "$lib/common/components/AppVariant.svelte";
  import { getContext } from "svelte";
  import type { TranslationService } from "$lib/translations/translation-service.svelte";

  const translationService = getContext<TranslationService>("translationService");

  async function importSheetClick(event: Event) {
    const input = event.currentTarget as HTMLInputElement;
    const file = input?.files?.item(0);

    if (file) {
      await translationService.importTranslationsSheet(file);
    }
  }
</script>

<label class="import">
  <AppVariant>
    <input accept=".xlsx" onchange={importSheetClick} style="display: none" type="file">
    Import
  </AppVariant>
</label>

<style>
    .import:hover {
        cursor: pointer;
    }
</style>
