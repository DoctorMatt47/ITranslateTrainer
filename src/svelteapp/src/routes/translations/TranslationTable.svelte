<script lang="ts">
  import TranslationRow from "./TranslationRow.svelte";
  import AddTranslationRow from "./AddTranslationRow.svelte";
  import { getContext } from "svelte";
  import type { TranslationService } from "$lib/translations/translation-service.svelte";

  const translationService = getContext<TranslationService>("translationService");

  const heads = ["#", "From", "To", "Original", "Translation", "Action"];
</script>

<div class="table-container">
  <table class="table dark:bg-black/0">
    <thead>
    <tr class="bg-surface-500/50">
      {#each heads as head}
        <th class="text-center">{head}</th>
      {/each}
    </tr>
    </thead>
    <tbody class="table-body">
    <AddTranslationRow />
    {#each translationService.translations as translation, rowIndex (translation.id)}
      <TranslationRow {translation} {rowIndex} />
    {/each}
    </tbody>
  </table>
</div>
