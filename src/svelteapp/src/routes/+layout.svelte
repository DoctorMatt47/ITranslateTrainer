<script lang="ts">
  // The ordering of these imports is critical to your app working properly
  import "@skeletonlabs/skeleton/themes/theme-gold-nouveau.css";
  // If you have source.organizeImports set to true in VSCode, then it will auto change this ordering
  import "@skeletonlabs/skeleton/styles/skeleton.css";
  // Most of your app wide CSS should be put in this file
  import "../app.postcss";
  import { AppBar, AppShell } from "@skeletonlabs/skeleton";
  import AppCard from "$lib/common/components/AppCard.svelte";

  const { children } = $props();
</script>

<AppShell>
  <AppBar slot="header">
    <svelte:fragment slot="lead">
      <i class="fa-solid fa-language"></i></svelte:fragment>
    ITranslateTrainer
    <svelte:fragment slot="trail">
      <a href="/menu"><i class="fa-solid fa-home"></i></a>
    </svelte:fragment>
  </AppBar>
  <div class="container mx-auto mt-8">
    <AppCard>
      <div class="flex flex-col gap-y-6">
        {@render children()}
      </div>
    </AppCard>
  </div>
</AppShell>
