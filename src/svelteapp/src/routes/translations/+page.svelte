<script lang="ts">
  import TranslationTable from "./TranslationTable.svelte";
  import AppHeading from "$lib/common/components/AppHeading.svelte";
  import AppVariant from "$lib/common/components/AppVariant.svelte";
  import ImportTranslationSheet from "./ImportTranslationSheet.svelte";
  import type { TranslationService } from "$lib/translations/translation-service.svelte";
  import { getContext } from "svelte";

  const translationService = getContext<TranslationService>("translationService");
</script>

<AppHeading>Translations</AppHeading>
<div class="grid grid-cols-2 gap-6">
  <ImportTranslationSheet />
  <AppVariant>Export</AppVariant>
</div>
{#await translationService.fetchTranslations()}
  <div class="mx-auto">Loading...</div>
{:then _}
  <TranslationTable />
{:catch error}
  <div class="mx-auto">{error.message}</div>
{/await}
