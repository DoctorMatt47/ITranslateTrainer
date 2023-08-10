<script lang="ts">
  import AppHeading from "$lib/components/AppHeading.svelte";
  import AppVariant from "$lib/components/AppVariant.svelte";
  import TranslationTable from "./TranslationTable.svelte";
  import { getTranslations } from "$lib/services/translation-service";
  import { translationsStore } from "$lib/stores";
  import ImportTranslationSheet from "./ImportTranslationSheet.svelte";
  import AddTranslationRow from "./AddTranslationRow.svelte";

  let translationsPromise = getTranslations().then((translations) => {
    translationsStore.set(translations);
    return translations;
  });

</script>

<AppHeading>Translations</AppHeading>

<div class="grid grid-cols-2 gap-6">
  <ImportTranslationSheet />
  <AppVariant>Export</AppVariant>
</div>

{#await translationsPromise}
  <div class="mx-auto">Loading...</div>

{:then translations}
  <TranslationTable translations={$translationsStore}>
    <AddTranslationRow />
  </TranslationTable>

{:catch error}
  <div class="mx-auto">{error.message}</div>

{/await}
