<script lang="ts">
  import AppVariant from "$lib/components/AppVariant.svelte";
  import { TranslationService } from "$lib/translations/translation-service";

  const translationService = new TranslationService();

  async function importSheetClick(event: Event) {
    const input = event.currentTarget as HTMLInputElement;
    const file = input?.files?.item(0);

    await translationService.importTranslationsSheet(file!);
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
